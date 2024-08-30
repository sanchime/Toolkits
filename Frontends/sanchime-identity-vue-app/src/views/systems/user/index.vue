<template>
    <dynamic-table 
        :selectable="true"
        :table-data="userList" 
        :columns="columns" 
        :operation="operation" 
        :page-total="pageTotal"
        :loading="loading"
        @load-table-data="loadTableData"></dynamic-table>

</template>

<script setup lang="ts">
import { ref } from "vue";
import api from '@/apis'
import { UserResponse } from "@/models/user"

const columns = [
    {
        prop: 'name',
        label: '名称',
        sortable: true
    },
    {
        prop: 'phone',
        label: '电话'
    },
    {
        prop: 'email',
        label: '邮件'
    },
    {
        prop: 'isEnabled',
        label: '是否启用',
        formatter: (row: UserResponse) => row.isEnabled ? "是" : "否"
    }
]

const operation = {
    columns: [
        {
            key: "edit",
            text: "编辑"
        },
        {
            key: "delete",
            text: "删除",
            type: "danger"
        }
    ]
}

const pageTotal = ref<number>()
let userList: UserResponse[]
const loading = ref<boolean>(false)
const loadTableData = async (newPageIndex: number = 1, newPageSize: number = 20) => {
    loading.value = true
    try {
        const pageResult = await api.user.getUsers(newPageIndex, newPageSize);
        pageTotal.value = pageResult.totalCount
        userList = pageResult.items
    }
    finally {
        loading.value = false
    }
}

</script>