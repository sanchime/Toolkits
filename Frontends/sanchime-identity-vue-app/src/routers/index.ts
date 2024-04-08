import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import { useTokenStore } from '@/store/token'
export const Layout = () => import("@/layouts/index.vue");

const routes: RouteRecordRaw[] = [
  {
    path: '/login',
    component: () => import('../views/Login.vue'),
    meta: { hidden: false, layout: false},
  },
  {
    path: '/',
    redirect: '/home',
  },
  {
    path: '/home',
    component: Layout,
    meta: { hidden: false },
    children: [{
      path: '/home',
      component: () => import('../views/Home.vue'),
      meta: { hidden: false },
    }]
  },
]

const router = createRouter({
  // NOTE: 路由历史模式 [参考](https://router.vuejs.org/zh/api/#history)
  history: createWebHashHistory(),
  routes,
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