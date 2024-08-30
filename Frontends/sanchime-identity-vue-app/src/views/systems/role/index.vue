<template>
    <dynamic-table :table-data="roleList" :columns="columns" :loading="loading"></dynamic-table>

</template>

<script setup lang="ts">
import { computed, inject, reactive, ref } from "vue";
import api from '@/apis'
import { RoleResponse } from "@/models/role"
import { PaginatedResult } from "@/models"

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
        prop: 'description',
        label: '备注'
    },
    {
        prop: 'isEnabled',
        label: '是否启用'
    }
]

let roleList: RoleResponse[]
const loading = ref<boolean>(false)
const loadTableData = async () => {
    loading.value = true
    try {
        roleList = await api.role.getRoles()
    }
    finally {
        loading.value = false
    }
}

loadTableData();


</script>