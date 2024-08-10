<template></template>

<script setup lang="ts">
import { computed, ref } from "vue";
import { Delete, Search, ArrowDown, ArrowUp } from "@element-plus/icons-vue";

interface SearchableProps {
  columns?: any[]; // 搜索配置列  
  searchParam?: { [key: string]: any }; // 搜索参数
  search: (params: any) => void; // 搜索方法
  reset: (params: any) => void; // 重置方法
}

// 默认值
const props = withDefaults(defineProps<SearchableProps>(), {
  columns: () => [],
  searchParam: () => ({}),
});

// 是否默认折叠搜索项
const collapsed = ref(true);
</script>
