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

  const itemCount = computed(() => items.value.length)
  const total = computed(() => items.value.reduce((sum, item) => sum + item.price, 0))
  const albumIds = computed(() => items.value.map(i => i.albumId))

  async function fetchCart(): Promise<void> {
    const response = await axios.get<{ cartId: string; items: CartItem[] }>(`/cart/${cartId.value}`)
    items.value = response.data.items
  }

  async function addToCart(album: Album): Promise<void> {
    const response = await axios.post<{ cartId: string; items: CartItem[] }>(
      `/cart/${cartId.value}/items`,
      { albumId: album.id }
    )
    items.value = response.data.items
  }

  async function removeFromCart(albumId: number): Promise<void> {
    const response = await axios.delete<{ cartId: string; items: CartItem[] }>(
      `/cart/${cartId.value}/items/${albumId}`
    )
    items.value = response.data.items
  }

  return { cartId, items, itemCount, total, albumIds, fetchCart, addToCart, removeFromCart }
})
