import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { ElementPlusResolver } from 'unplugin-vue-components/resolvers'
import { resolve } from "path";
import Icons from "unplugin-icons/vite";
import IconsResolver from "unplugin-icons/resolver";
import { createSvgIconsPlugin } from "vite-plugin-svg-icons";
import path from "path";
import UnoCSS from "unocss/vite";

const pathSrc = path.resolve(__dirname, "src");

// https://vitejs.dev/config/
export default defineConfig({

  plugins: [
    vue(),
    UnoCSS({
      configFile: './uno.config.ts',
    }),
    AutoImport({
      imports: ["vue", "@vueuse/core"],
      resolvers: [
        ElementPlusResolver(),
        IconsResolver(),
      ],
      vueTemplate: true,
    }),
    Components({
      resolvers: [
        // 自动导入 Element Plus 组件
        ElementPlusResolver(),
        // 自动导入图标组件
        IconsResolver({
          // @iconify-json/ep 是 Element Plus 的图标库
          enabledCollections: ["ep"],
        })
      ],
       // 指定自定义组件位置(默认:src/components)
       dirs: ["src/**/components"],
       // 配置文件位置(false:关闭自动生成)
       dts: false,
       // dts: "src/types/components.d.ts",
    }),
    Icons({
      // 自动安装图标库
      autoInstall: true,
    }),

    createSvgIconsPlugin({
      // 指定需要缓存的图标文件夹
      iconDirs: [path.resolve(pathSrc, "assets/icons")],
      // 指定symbolId格式
      symbolId: "icon-[dir]-[name]",
    }),
  ],
  resolve: {
    "alias": {
      "@": resolve(__dirname, "./src"),
    }
  },
  css: {
    // CSS 预处理器
    preprocessorOptions: {
      //define global scss variable
      scss: {
        javascriptEnabled: true,
        additionalData: `
          @use "@/styles/variables.scss" as *;
        `,
      },
    },
  },

  optimizeDeps: {

    include: [
      "vue",
      "vue-router",
      "pinia",
      "element-plus/es/components/form/style/css",
      "element-plus/es/components/form-item/style/css",
      "element-plus/es/components/button/style/css",
      "element-plus/es/components/input/style/css",
      "element-plus/es/components/input-number/style/css",
      "element-plus/es/components/switch/style/css",
      "element-plus/es/components/upload/style/css",
      "element-plus/es/components/menu/style/css",
      "element-plus/es/components/col/style/css",
      "element-plus/es/components/icon/style/css",
      "element-plus/es/components/row/style/css",
      "element-plus/es/components/tag/style/css",
      "element-plus/es/components/dialog/style/css",
      "element-plus/es/components/loading/style/css",
      "element-plus/es/components/radio/style/css",
      "element-plus/es/components/radio-group/style/css",
      "element-plus/es/components/popover/style/css",
      "element-plus/es/components/scrollbar/style/css",
      "element-plus/es/components/tooltip/style/css",
      "element-plus/es/components/dropdown/style/css",
      "element-plus/es/components/dropdown-menu/style/css",
      "element-plus/es/components/dropdown-item/style/css",
      "element-plus/es/components/sub-menu/style/css",
      "element-plus/es/components/menu-item/style/css",
      "element-plus/es/components/divider/style/css",
      "element-plus/es/components/card/style/css",
      "element-plus/es/components/link/style/css",
      "element-plus/es/components/breadcrumb/style/css",
      "element-plus/es/components/breadcrumb-item/style/css",
      "element-plus/es/components/table/style/css",
      "element-plus/es/components/tree-select/style/css",
      "element-plus/es/components/table-column/style/css",
      "element-plus/es/components/select/style/css",
      "element-plus/es/components/option/style/css",
      "element-plus/es/components/pagination/style/css",
      "element-plus/es/components/tree/style/css",
      "element-plus/es/components/alert/style/css",
      "@vueuse/core",
    ]
  }
})
