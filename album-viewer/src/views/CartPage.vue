<template>
  <div class="cart-page">
    <div class="cart-header">
      <RouterLink to="/" class="back-link">← Continue Shopping</RouterLink>
      <h2>Your Cart <span v-if="cartStore.itemCount > 0">({{ cartStore.itemCount }})</span></h2>
    </div>

    <div v-if="cartStore.error" class="cart-error">
      ⚠️ {{ cartStore.error }}
    </div>

    <div v-if="cartStore.items.length === 0" class="empty-cart">
      <p>🛒 Your cart is empty.</p>
      <RouterLink to="/" class="browse-btn">Browse Albums</RouterLink>
    </div>

    <div v-else class="cart-content">
      <div class="cart-items">
        <div v-for="item in cartStore.items" :key="item.albumId" class="cart-item">
          <img :src="item.imageUrl" :alt="item.title" class="item-image" @error="handleImageError" />
          <div class="item-details">
            <h3>{{ item.title }}</h3>
            <p class="item-artist">{{ item.artist }}</p>
          </div>
          <div class="item-price">${{ item.price.toFixed(2) }}</div>
          <button class="remove-btn" @click="cartStore.removeFromCart(item.albumId)" title="Remove">✕</button>
        </div>
      </div>

      <div class="cart-summary">
        <div class="summary-row">
          <span>{{ cartStore.itemCount }} {{ cartStore.itemCount === 1 ? 'album' : 'albums' }}</span>
          <span class="total">${{ cartStore.total.toFixed(2) }}</span>
        </div>
        <button class="checkout-btn">Proceed to Checkout</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { useCartStore } from '../stores/cart'

const cartStore = useCartStore()

onMounted(() => {
  cartStore.fetchCart()
})

const handleImageError = (event: Event): void => {
  const target = event.target as HTMLImageElement
  target.src = 'https://via.placeholder.com/80x80/667eea/white?text=Album'
}
</script>

<style scoped>
.cart-page {
  max-width: 800px;
  margin: 0 auto;
  color: white;
}

.cart-header {
  margin-bottom: 2rem;
}

.back-link {
  color: rgba(255, 255, 255, 0.8);
  text-decoration: none;
  font-size: 0.95rem;
  display: inline-block;
  margin-bottom: 0.75rem;
  transition: color 0.2s;
}

.back-link:hover {
  color: white;
}

.cart-header h2 {
  font-size: 2rem;
  margin: 0;
  text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
}

.empty-cart {
  text-align: center;
  padding: 4rem 2rem;
}

.empty-cart p {
  font-size: 1.4rem;
  margin-bottom: 1.5rem;
  opacity: 0.9;
}

.browse-btn {
  display: inline-block;
  background: rgba(255, 255, 255, 0.2);
  color: white;
  border: 2px solid white;
  padding: 0.75rem 2rem;
  border-radius: 25px;
  font-size: 1rem;
  text-decoration: none;
  transition: all 0.3s ease;
}

.browse-btn:hover {
  background: white;
  color: #667eea;
}

.cart-error {
  background: rgba(229, 62, 62, 0.25);
  border: 1px solid rgba(229, 62, 62, 0.6);
  color: white;
  border-radius: 8px;
  padding: 0.75rem 1rem;
  margin-bottom: 1.25rem;
  font-size: 0.95rem;
}

.cart-items {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  margin-bottom: 2rem;
}

.cart-item {
  display: flex;
  align-items: center;
  gap: 1.25rem;
  background: rgba(255, 255, 255, 0.12);
  border-radius: 12px;
  padding: 1rem 1.25rem;
  backdrop-filter: blur(10px);
}

.item-image {
  width: 70px;
  height: 70px;
  border-radius: 8px;
  object-fit: cover;
  flex-shrink: 0;
}

.item-details {
  flex: 1;
}

.item-details h3 {
  margin: 0 0 0.25rem;
  font-size: 1.1rem;
}

.item-artist {
  margin: 0;
  opacity: 0.75;
  font-size: 0.9rem;
}

.item-price {
  font-size: 1.2rem;
  font-weight: bold;
  flex-shrink: 0;
}

.remove-btn {
  background: rgba(255, 255, 255, 0.15);
  color: white;
  border: none;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  cursor: pointer;
  font-size: 0.85rem;
  flex-shrink: 0;
  transition: background 0.2s;
}

.remove-btn:hover {
  background: rgba(255, 80, 80, 0.6);
}

.cart-summary {
  background: rgba(255, 255, 255, 0.12);
  border-radius: 12px;
  padding: 1.5rem;
  backdrop-filter: blur(10px);
}

.summary-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.25rem;
  font-size: 1.1rem;
}

.total {
  font-size: 1.6rem;
  font-weight: bold;
}

.checkout-btn {
  width: 100%;
  padding: 1rem;
  background: white;
  color: #667eea;
  border: none;
  border-radius: 10px;
  font-size: 1.1rem;
  font-weight: 700;
  cursor: pointer;
  transition: all 0.3s ease;
}

.checkout-btn:hover {
  background: rgba(255, 255, 255, 0.9);
  transform: translateY(-2px);
}
</style>
