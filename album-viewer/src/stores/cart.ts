import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import axios from 'axios'
import type { Album } from '../types/album'

export interface CartItem {
  albumId: number
  title: string
  artist: string
  price: number
  imageUrl: string
}

function getOrCreateCartId(): string {
  let cartId = localStorage.getItem('cartId')
  if (!cartId) {
    cartId = crypto.randomUUID()
    localStorage.setItem('cartId', cartId)
  }
  return cartId
}

export const useCartStore = defineStore('cart', () => {
  const cartId = ref<string>(getOrCreateCartId())
  const items = ref<CartItem[]>([])
  const error = ref<string | null>(null)
  const loading = ref<boolean>(false)

  const itemCount = computed(() => items.value.length)
  // Round to 2 decimal places to avoid IEEE 754 float accumulation errors
  const total = computed(() =>
    Math.round(items.value.reduce((sum, item) => sum + item.price, 0) * 100) / 100
  )
  const albumIds = computed(() => items.value.map(i => i.albumId))

  async function fetchCart(): Promise<void> {
    try {
      loading.value = true
      error.value = null
      const response = await axios.get<{ cartId: string; items: CartItem[] }>(`/cart/${cartId.value}`)
      items.value = response.data.items
    } catch (err) {
      error.value = 'Failed to load cart. Please try again.'
      console.error('Error fetching cart:', err)
    } finally {
      loading.value = false
    }
  }

  async function addToCart(album: Album): Promise<void> {
    try {
      loading.value = true
      error.value = null
      const response = await axios.post<{ cartId: string; items: CartItem[] }>(
        `/cart/${cartId.value}/items`,
        { albumId: album.id }
      )
      items.value = response.data.items
    } catch (err) {
      error.value = 'Failed to add album to cart.'
      console.error('Error adding to cart:', err)
    } finally {
      loading.value = false
    }
  }

  async function removeFromCart(albumId: number): Promise<void> {
    try {
      loading.value = true
      error.value = null
      const response = await axios.delete<{ cartId: string; items: CartItem[] }>(
        `/cart/${cartId.value}/items/${albumId}`
      )
      items.value = response.data.items
    } catch (err) {
      error.value = 'Failed to remove album from cart.'
      console.error('Error removing from cart:', err)
    } finally {
      loading.value = false
    }
  }

  return { cartId, items, itemCount, total, albumIds, error, loading, fetchCart, addToCart, removeFromCart }
})
