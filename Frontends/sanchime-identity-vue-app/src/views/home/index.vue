<template>
    <div class="dashboard">
        <!-- 用户信息 -->
    <el-row class="mb-8">
      <el-card class="w-full">
        <div class="flex justify-between flex-wrap">
          <div class="flex items-center">
            <img
              class="user-avatar"
              :src="tokenStore.user.userAvatar + '?imageView2/1/w/80/h/80'"
            />
            <span class="ml-[10px] text-[16px]">
              {{ tokenStore.user.userName }}
            </span>
          </div>

          <div class="leading-[40px]">
            {{ greetings }}
          </div>

        </div>
      </el-card>
    </el-row>
    </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useTokenStore } from "@/store/token";
import { useTransition, TransitionPresets } from "@vueuse/core";

const tokenStore = useTokenStore();

const date: Date = new Date();

const greetings = computed(() => {
  if (date.getHours() >= 6 && date.getHours() < 8) {
    return "晨起披衣出草堂，轩窗已自喜微凉🌅！";
  } else if (date.getHours() >= 8 && date.getHours() < 12) {
    return "薄暮焚香临野烧，清晨漱齿涉寒流🌞！";
  } else if (date.getHours() >= 12 && date.getHours() < 18) {
    return "故人相忆僧来说，杨柳无风蝉满枝☕！";
  } else if (date.getHours() >= 18 && date.getHours() < 24) {
    return "更深月色半人家，北斗阑干南斗斜🌃！";
  } else if (date.getHours() >= 0 && date.getHours() < 6) {
    return "醉后不知天在水，满船清梦压星河🌛！";
  }
});
</script>

<style lang="scss" scoped>
.dashboard {
  position: relative;
  padding: 24px;
}
</style>