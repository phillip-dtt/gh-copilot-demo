<template>
  <Teleport to="body">
    <Transition name="sidebar">
      <div v-if="isOpen" class="sidebar-overlay" @click.self="$emit('close')">
        <div class="sidebar">
          <div class="sidebar-header">
            <h2>🛒 Cart <span v-if="cartStore.itemCount > 0" class="count-badge">{{ cartStore.itemCount }}</span></h2>
            <button class="close-btn" @click="$emit('close')">✕</button>
          </div>

          <div v-if="cartStore.items.length === 0" class="empty-state">
            <p>Your cart is empty.</p>
          </div>

          <div v-else class="sidebar-items">
            <div v-for="item in cartStore.items" :key="item.albumId" class="sidebar-item">
              <img :src="item.imageUrl" :alt="item.title" class="item-thumb" @error="handleImageError" />
              <div class="item-info">
                <p class="item-title">{{ item.title }}</p>
                <p class="item-artist">{{ item.artist }}</p>
                <p class="item-price">${{ item.price.toFixed(2) }}</p>
              </div>
              <button class="remove-btn" @click="cartStore.removeFromCart(item.albumId)" title="Remove">✕</button>
            </div>
          </div>

          <div v-if="cartStore.items.length > 0" class="sidebar-footer">
            <div class="total-row">
              <span>Total</span>
              <strong>${{ cartStore.total.toFixed(2) }}</strong>
            </div>
            <RouterLink to="/cart" class="view-cart-btn" @click="$emit('close')">View Full Cart</RouterLink>
          </div>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>

<script setup lang="ts">
import { useCartStore } from '../stores/cart'

defineProps<{ isOpen: boolean }>()
defineEmits<{ close: [] }>()

const cartStore = useCartStore()

const handleImageError = (event: Event): void => {
  const target = event.target as HTMLImageElement
  target.src = 'https://via.placeholder.com/50x50/667eea/white?text=Album'
}
</script>

<style scoped>
.sidebar-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 1000;
  display: flex;
  justify-content: flex-end;
}

.sidebar {
  width: 380px;
  max-width: 100vw;
  background: #1a1a2e;
  height: 100%;
  display: flex;
  flex-direction: column;
  box-shadow: -4px 0 20px rgba(0, 0, 0, 0.4);
}

.sidebar-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1.25rem 1.5rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
  color: white;
}

.sidebar-header h2 {
  margin: 0;
  font-size: 1.3rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.count-badge {
  background: #667eea;
  color: white;
  border-radius: 50%;
  width: 24px;
  height: 24px;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  font-size: 0.8rem;
  font-weight: 700;
}

.close-btn {
  background: none;
  border: none;
  color: rgba(255, 255, 255, 0.7);
  font-size: 1.1rem;
  cursor: pointer;
  padding: 0.25rem;
  border-radius: 4px;
  transition: color 0.2s;
}

.close-btn:hover {
  color: white;
}

.empty-state {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  color: rgba(255, 255, 255, 0.6);
  font-size: 1rem;
}

.sidebar-items {
  flex: 1;
  overflow-y: auto;
  padding: 1rem 1.5rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.sidebar-item {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  color: white;
}

.item-thumb {
  width: 50px;
  height: 50px;
  border-radius: 6px;
  object-fit: cover;
  flex-shrink: 0;
}

.item-info {
  flex: 1;
  min-width: 0;
}

.item-title {
  margin: 0 0 0.1rem;
  font-size: 0.9rem;
  font-weight: 600;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.item-artist {
  margin: 0 0 0.1rem;
  font-size: 0.8rem;
  opacity: 0.65;
}

.item-price {
  margin: 0;
  font-size: 0.85rem;
  color: #a78bfa;
  font-weight: 600;
}

.remove-btn {
  background: rgba(255, 255, 255, 0.1);
  color: rgba(255, 255, 255, 0.6);
  border: none;
  width: 26px;
  height: 26px;
  border-radius: 50%;
  cursor: pointer;
  font-size: 0.75rem;
  flex-shrink: 0;
  transition: all 0.2s;
}

.remove-btn:hover {
  background: rgba(255, 80, 80, 0.5);
  color: white;
}

.sidebar-footer {
  padding: 1.25rem 1.5rem;
  border-top: 1px solid rgba(255, 255, 255, 0.1);
}

.total-row {
  display: flex;
  justify-content: space-between;
  color: white;
  font-size: 1.05rem;
  margin-bottom: 1rem;
}

.total-row strong {
  font-size: 1.3rem;
  color: #a78bfa;
}

.view-cart-btn {
  display: block;
  text-align: center;
  background: #667eea;
  color: white;
  text-decoration: none;
  padding: 0.85rem;
  border-radius: 10px;
  font-weight: 600;
  font-size: 1rem;
  transition: background 0.2s;
}

.view-cart-btn:hover {
  background: #5a6fd8;
}

/* Slide transition */
.sidebar-enter-active,
.sidebar-leave-active {
  transition: opacity 0.25s ease;
}

.sidebar-enter-active .sidebar,
.sidebar-leave-active .sidebar {
  transition: transform 0.25s ease;
}

.sidebar-enter-from,
.sidebar-leave-to {
  opacity: 0;
}

.sidebar-enter-from .sidebar,
.sidebar-leave-to .sidebar {
  transform: translateX(100%);
}
</style>
