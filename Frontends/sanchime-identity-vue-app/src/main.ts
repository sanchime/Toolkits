import { createApp } from 'vue'
import App from './App.vue'
import pinia from './store'
import router from './routers'
import ElementPlus from 'element-plus';
import 'element-plus/theme-chalk/dark/css-vars.css';
import 'uno.css';
import '@/styles/index.scss';

const app = createApp(App)

app
    .use(ElementPlus)
    .use(router)
    .use(pinia)
    .mount('#app')
