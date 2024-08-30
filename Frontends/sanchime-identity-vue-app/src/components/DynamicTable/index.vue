<template>

<div v-loading="props.loading" class="dynamic-table">

    <div v-if="props.operation?.headers" class="p-14">
        
        <slot name="header" :rows="selectionList">
            <span v-for="item in props.operation?.headers" :key="item.key">
                <el-button
                    :type="item.type || 'primary'"
                    :link="item.link"
                    :plain="item.plain"
                    @click="item.click(selectionList)"
                    style="margin-left: 10px;"
                >
                    <el-icon v-if="item.icon" :class="item.icon"></el-icon>
                    {{ item.text }}
                </el-button>
            </span>
        </slot>

    </div>

    <el-table 
        :data="props.tableData" 
        :row-key="props.rowKey"
        :tree-props="{ children: props.childrenLabel  }">

        <!-- 选择复选框 -->
        <el-table-column fixed :selectable="setSelectable" type="selection" v-if="showSelectBox"></el-table-column>

        <el-table-column 
            v-for="item in props.columns" 
            :key="item.prop"
            :prop="item.prop"
            :label="item.label"
            :sortable="item.sortable ? 'custom': false"
            :width="item.width"
        >   

        <!-- 表头 -->
        <template #header>
            <slot name="header">
                <div class="inline-flex" :style="item.labelStyle">
                    <span>{{ item.label }}</span>
                    <el-tooltip 
                        popper-class="table-tooltip" 
                        :content="item.tooltip" 
                        v-if="item.tooltip">
                        <el-icon><i-ep-warning></i-ep-warning></el-icon>
                    </el-tooltip>
                </div>
            </slot>
        </template>


        <template #default="scope">
            <slot :name="item.prop" :row="scope.row">
                <div :style="item.style">
                    <span v-if="item.formatter">{{ item.formatter(scope.row) }}</span>
                    <span v-else>{{ scope.row[item.prop] }}</span>
                </div>
            </slot>
        </template>

        </el-table-column>

        <!-- 操作列 -->
        <el-table-column 
            fixed="right" 
            label="操作"
            :v-if="!!props.operation?.columns"
            >
           
            <template #default="scope">
                <slot name="operations" :row="scope.row">
                    <span v-for="item in props.operation?.columns" :key="item.key">
                        <el-button 
                            :type="item.type || 'primary'"
                            :link="item.link"
                            :plain="item.plain"
                            size="small"
                            style="margin-right: 4px;"
                            @click="item.click(scope.row)">
                            <el-icon v-if="item.icon" :class="item.icon"></el-icon>
                            {{ item.text }}
                        </el-button>
                    </span>
                </slot>
            </template>

        </el-table-column>
    </el-table>

    <div v-if="showSelectBox && props.operation?.footers" class="p-14">
        
        <slot name="footer" :rows="selectionList">
            <span v-for="item in props.operation?.footers" :key="item.key">
                <el-button
                    :type="item.type || 'primary'"
                    :link="item.link"
                    :plain="item.plain"
                    :disabled="!selectionList.length"
                    @click="item.click(selectionList)"
                    style="margin-left: 10px;"
                >
                    <el-icon v-if="item.icon" :class="item.icon"></el-icon>
                    {{ item.text }}
                </el-button>
            </span>
        </slot>

    </div>

    <el-pagination 
        :layout="props.layout"
        v-model:current-page="pageIndex"
        v-model:page-size="pageSize"
        :total="props.pageTotal"
        @current-change="loadTableData"
        @size-change="loadTableData"
        class="p-y-20"
    >

    </el-pagination>
</div>
    

</template>

<script setup lang="ts">
import { computed, reactive, ref } from 'vue';

interface TableColumn {
    prop: string,
    label?: string
    formatter?: (row: unknown) => string,
    tooltip?: string,
    sortable?: boolean,
    width: number | string,
    style: string
    labelStyle?: string
}

interface TableOperation {
    click: (row: unknown) => void,
    key: string,
    text?: string,
    icon?: string,
    type?: string,
    link?: string,
    plain?: string,
    visible?: (row?: unknown) => boolean
}

interface TableConfiguration {
    rowKey?: string,
    columns: TableColumn[],
    operation?: {
        columns: TableOperation[],
        headers: TableOperation[],
        footers: TableOperation[],
    },
    tableData: unknown[],
    loading: boolean,
    childrenLabel: string,
    selectable: boolean | ((row: unknown) => boolean),
    layout: string,
    pageIndex: number,
    pageSize: number,
    pageTotal: number
}
const pageIndex = ref<number>(1)
const pageSize = ref<number>(20)
const props = withDefaults(defineProps<TableConfiguration>(), {
    rowKey: 'id',
    childrenLabel: "children",
    layout: "prev, pager, next, total"
})

const emit = defineEmits(['loadTableData'])

const loadTableData = () => {
    emit("loadTableData", pageIndex.value, pageSize.value)
}

// 禁止勾选的数据列表
const disableSelectionList = reactive<string[]>([])
const selectionList = reactive<unknown[]>([])
const showSelectBox = computed(() => props.selectable && disableSelectionList.length < props.tableData?.length)
const setSelectable = (row: any) => {
    const selectable = typeof props.selectable === "boolean" ? props.selectable : props.selectable?.(row)

    if (!selectable && !disableSelectionList.includes(row?.[props.rowKey])) {
        disableSelectionList.push(row?.[props.rowKey])
    }

    return selectable
}

loadTableData();

</script>

<style scoped lang="scss">
.dynamic-table {
    border-top: 1px solid #eaeaea;
    border-left: 1px solid #eaeaea;
    border-right: 1px solid #eaeaea;
}

.inline-flex {
    display: inline-flex;
    align-items: center;
}

.p-14 {
    border-bottom: 1px solid #eaeaea;
    padding: 14px;
}

.p-y-20 {
    padding-top: 20px;
    padding-bottom: 20px;
    justify-content: center;
}

</style>