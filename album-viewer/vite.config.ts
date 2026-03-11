import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src')
    }
  },
  server: {
    port: 3001,
    proxy: {
      '/albums': {
        target: 'http://localhost:3000',
        changeOrigin: true
      },
      '^/cart/[0-9a-f]{8}-[0-9a-f]{4}': {
        target: 'http://localhost:3000',
        changeOrigin: true
      }
    }
  }
})
