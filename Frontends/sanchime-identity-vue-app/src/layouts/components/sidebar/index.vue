<template>
  <div :class="{ 'has-logo': sidebarLogo }">
    <logo v-if="sidebarLogo" :collapse="!appStore.sidebar.opened" />
    <el-scrollbar>
      <el-menu
        :default-active="currRoute.path"
        :collapse="!appStore.sidebar.opened"
        :background-color="variables.menuBg"
        :text-color="variables.menuText"
        :active-text-color="variables.menuActiveText"
        :unique-opened="false"
        :collapse-transition="false"
        mode="vertical"
      >
        <sidebar-item
          v-for="route in routes"
          :key="route.meta.id"
          :item="route"
          :base-path="route.path"
          :is-collapse="!appStore.sidebar.opened"
        />
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { RouteRecordRaw, useRoute } from "vue-router";

import variables from "@/styles/variables.module.scss";
import { getRouters } from "@/routers/menu";
import { useSettingsStore } from "@/store/settings";
import { useAppStore } from "@/store/app";
import { storeToRefs } from "pinia";
const settingsStore = useSettingsStore();
const appStore = useAppStore();
const { sidebarLogo } = storeToRefs(settingsStore);
const currRoute = useRoute();
// 创建一个响应式引用来存储路由
const routes = ref<RouteRecordRaw[]>([]);

// 使用onMounted生命周期钩子来获取路由数据
onMounted(async () => {
  routes.value = await getRouters();
});
</script>
