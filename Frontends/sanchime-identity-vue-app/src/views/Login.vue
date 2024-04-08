<template>
  <div class="login">
    <el-form
      :model="loginRequest"
      :rules="loginRules"
      ref="loginForm"
      class="login-form"
    >
      <div class="flex text-white items-center py-4">
        <span class="text-2xl flex-1 text-center">系统登录</span>
        <lang-select class="text-white! cursor-pointer" />
      </div>
      <el-form-item prop="account">
        <div class="p-2 text-white">
          <svg-icon icon-class="user" />
        </div>
        <el-input
          type="text"
          class="flex-1"
          name="account"
          v-model="loginRequest.account"
          placeholder="请输入登录账号"
        ></el-input>
      </el-form-item>

      <el-tooltip
        :disabled="isCapslock === false"
        content="Caps lock is On"
        placement="right"
      >
        <el-form-item prop="password">
          <span class="p-2 text-white">
            <svg-icon icon-class="password" />
          </span>
          <el-input
            class="flex-1"
            :type="passwordVisible === false ? 'password' : 'input'"
            v-model="loginRequest.password"
            placeholder="请输入密码"
            size="large"
            name="password"
            @keyup="checkCapslock"
            @keyup.enter="onLogin"
          ></el-input>

          <span class="mr-2" @click="passwordVisible = !passwordVisible">
            <svg-icon
              :icon-class="passwordVisible === false ? 'eye' : 'eye-open'"
              class="text-white cursor-pointer"
            />
          </span>
        </el-form-item>
      </el-tooltip>
      <el-button
        type="primary"
        size="default"
        style="width: 100%"
        class="w-full"
        @click.prevent="onLogin"
        :loading="loading"
        >登录</el-button
      >
    </el-form>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from "vue";
import router from "@/routers";
import type { LoginRequest } from "@/models/index";
import { useTokenStore } from "@/store/token";
import api from "@/apis/index";
import type { FormInstance, FormRules } from "element-plus";
import { error, success } from "@/utils/notification";
import SvgIcon from "@/components/SvgIcon/index.vue";
const loginRequest = reactive<LoginRequest>({ account: "", password: "" });
const loading = ref<boolean>(false);
const loginForm = ref<FormInstance>();
const loginRules = reactive<FormRules>({
  account: [{ required: true, message: "请输入账号", trigger: "input" }],
  password: [{ required: true, message: "请输入密码", trigger: "input" }],
});
const passwordVisible = ref(false);
const isCapslock = ref(false);

const checkCapslock = (e: any) => {
  const { key } = e;
  isCapslock.value = key && key.length === 1 && key >= "A" && key <= "Z";
};

async function onLogin() {
  if (!loginForm.value) return;

  await loginForm.value.validate(async (valid, fields) => {
    if (!valid) {
      console.log(fields);
      error("请检查输入项");
      return;
    }
    loading.value = true;
    try {
      const response = await api.account.login(loginRequest);
      if (!!response) {
        useTokenStore().setToken(response);
        success("登录成功");

        router.push("/");
      }
    } finally {
      loading.value = false;
    }
  });
}
</script>

<style scoped lang="scss">
.login {
  width: 100%;
  min-height: 100%;
  overflow: hidden;
  background-color: #2d3a4b;

  margin-bottom: 20px;
  font-size: 26px;
  text-align: center;

  .form {
    width: 520px;
    max-width: 100%;
    padding: 160px 35px 0;
    margin: 0 auto;
    overflow: hidden;
  }
}

.el-form-item {
  background: rgb(0 0 0 / 10%);
  border: 1px solid rgb(255 255 255 / 10%);
  border-radius: 5px;
}

.el-input {
  background: transparent;

  // 子组件 scoped 无效，使用 :deep
  :deep(.el-input__wrapper) {
    padding: 0;
    background: transparent;
    box-shadow: none;

    .el-input__inner {
      color: #fff;
      background: transparent;
      border: 0;
      border-radius: 0;
      caret-color: #fff;

      &:-webkit-autofill {
        box-shadow: 0 0 0 1000px transparent inset !important;
        -webkit-text-fill-color: #fff !important;
      }

      // 设置输入框自动填充的延迟属性
      &:-webkit-autofill,
      &:-webkit-autofill:hover,
      &:-webkit-autofill:focus,
      &:-webkit-autofill:active {
        transition: color 99999s ease-out, background-color 99999s ease-out;
        transition-delay: 99999s;
      }
    }
  }
}
</style>
