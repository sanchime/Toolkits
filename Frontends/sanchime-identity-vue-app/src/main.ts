import { createApp } from 'vue'
import App from './App.vue'
import pinia from './store'
import router from './routers'
import ElementPlus from 'element-plus';
import 'element-plus/theme-chalk/dark/css-vars.css';
import 'uno.css';
import '@/styles/index.scss';
// 本地SVG图标
import 'virtual:svg-icons-register';

const app = createApp(App)

app
    .use(ElementPlus)
    .use(router)
    .use(pinia)
    .mount('#app')
