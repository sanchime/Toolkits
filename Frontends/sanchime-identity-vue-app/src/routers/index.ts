import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import { useTokenStore } from '@/store/token'
export const Layout = () => import("@/layouts/index.vue");

export const routes: RouteRecordRaw[] = [
  {
    path: '/login',
    component: () => import('../views/Login.vue'),
    meta: { hidden: false, layout: false},
  },
  {
    path: "/401",
    component: () => import("@/views/error/401.vue"),
    meta: { hidden: true },
  },
  {
    path: "/404",
    component: () => import("@/views/error/404.vue"),
    meta: { hidden: true },
  },
  {
    path: '/',
    redirect: '/home',
    component: Layout,
    meta: { hidden: false },
    children: [{
      path: '/home',
      component: () => import('../views/home/index.vue'),
      meta: { title: "home", hidden: false, icon: "homepage", affix: true },
    }]
  },
]

const router = createRouter({
  // NOTE: 路由历史模式 [参考](https://router.vuejs.org/zh/api/#history)
  history: createWebHashHistory(),
  routes,

  // 刷新时，滚动条位置还原
  scrollBehavior: () => ({ left: 0, top: 0 }),
})

router.beforeEach((to, from, next) => {
  const store = useTokenStore()
  if (to.path !== '/login' && !store.state.token) {
    return next('/login')
  } else if (to.path === '/login' && store.state.token) {
    return next()
  }

  return next()
})

export default router