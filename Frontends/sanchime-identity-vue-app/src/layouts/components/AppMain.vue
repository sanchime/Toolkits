<template>
  <section class="app-main">
    <router-view v-slot="{ Component, route }">
      <transition name="router-fade" mode="out-in">
        <!-- 不需要缓存 -->
        <!-- <keep-alive :include="tagsViewStore.cachedViews">
          
        </keep-alive> -->
        <component :is="Component" :key="route.fullPath" />
      </transition>
    </router-view>
  </section>
</template>

<script setup lang="ts">
// import { useTagsViewStore } from "@/store/modules/tagsView";

// const tagsViewStore = useTagsViewStore();
</script>

<style lang="scss" scoped>
.app-main {
  position: relative;
  width: 100%;

  /* 50= navbar  50  */
  min-height: calc(100vh - 50px);
  overflow: hidden;
  background-color: var(--el-bg-color-page);
}

.fixed-header + .app-main {
  padding-top: 50px;
}

.hasTagsView {
  .app-main {
    /* 84 = navbar + tags-view = 50 + 34 */
    min-height: calc(100vh - 84px);
  }

  .fixed-header + .app-main {
    min-height: 100vh;
    padding-top: 84px;
  }
}
</style>
