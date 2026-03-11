<template>
  <div class="app">
    <header class="header">
      <div class="header-content">
        <div class="header-text">
          <RouterLink to="/" class="header-link">
            <h1>🎵 Album Collection</h1>
          </RouterLink>
          <p>Discover amazing music albums</p>
        </div>
        <button class="cart-btn" @click="sidebarOpen = true">
          🛒
          <span v-if="cartStore.itemCount > 0" class="cart-badge">{{ cartStore.itemCount }}</span>
        </button>
      </div>
    </header>

    <main class="main">
      <RouterView />
    </main>

    <CartSidebar :is-open="sidebarOpen" @close="sidebarOpen = false" />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useCartStore } from './stores/cart'
import CartSidebar from './components/CartSidebar.vue'

const sidebarOpen = ref(false)
const cartStore = useCartStore()

onMounted(() => {
  cartStore.fetchCart()
})
</script>

<style scoped>
.app {
  min-height: 100vh;
  padding: 2rem;
}

.header {
  margin-bottom: 3rem;
  color: white;
}

.header-content {
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  max-width: 1200px;
  margin: 0 auto;
}

.header-text {
  text-align: center;
}

.header-link {
  text-decoration: none;
  color: inherit;
}

.header h1 {
  font-size: 3rem;
  margin-bottom: 0.5rem;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
}

.header p {
  font-size: 1.2rem;
  opacity: 0.9;
  margin: 0;
}

.cart-btn {
  position: absolute;
  right: 0;
  background: rgba(255, 255, 255, 0.2);
  border: 2px solid rgba(255, 255, 255, 0.6);
  color: white;
  font-size: 1.4rem;
  width: 52px;
  height: 52px;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.3s ease;
}

.cart-btn:hover {
  background: rgba(255, 255, 255, 0.35);
}

.cart-badge {
  position: absolute;
  top: -6px;
  right: -6px;
  background: #e53e3e;
  color: white;
  border-radius: 50%;
  width: 22px;
  height: 22px;
  font-size: 0.75rem;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
}

.main {
  max-width: 1200px;
  margin: 0 auto;
}

@media (max-width: 768px) {
  .app {
    padding: 1rem;
  }

  .header h1 {
    font-size: 2rem;
  }
}
</style>

