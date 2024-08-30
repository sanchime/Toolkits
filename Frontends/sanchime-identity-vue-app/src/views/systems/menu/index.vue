<template>
    <dynamic-table 
        :table-data="menuList" 
        :operation="operation" 
        :columns="columns" 
        :loading="loading"></dynamic-table>

</template>

<script setup lang="ts">
import { computed, inject, reactive, ref } from "vue";
import api from '@/apis'
import { MenuTreeResponse } from "@/models";

const columns = [
    {
        prop: 'name',
        label: '名称'
    },
    {
        prop: 'code',
        label: '编码'
    },
    {
        prop: 'order',
        label: '顺序',
        sortable: true
    },
    {
        prop: 'icon',
        label: '图标'
    },
    {
        prop: 'route',
        label: '路由'
    },
    {
        prop: 'path',
        label: '路径'
    },
    {
        prop: 'description',
        label: '备注'
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
    ],
    headers: [
        {
            key: "add",
            text: "新增",
            visible: true
        }
    ]
}

let menuList: MenuTreeResponse[]
const loading = ref<boolean>(false)
const loadTableData = async () => {
    loading.value = true
    try {
        menuList = await api.menu.getMenuTree()
        console.log(menuList)
    }
    finally {
        loading.value = false
    }
}

loadTableData();


</script>