import { createRouter, createWebHistory } from 'vue-router'
import AlbumsView from '../views/AlbumsView.vue'
import CartPage from '../views/CartPage.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: AlbumsView },
    { path: '/cart', component: CartPage }
  ]
})

export default router
