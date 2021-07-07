<template>
  <div id="nav">
    <div class="title">
      <span @click="goHome">
        Book Catalog
      </span>
    </div>
    <div class="items">
      <router-link to="/">
        Home
      </router-link>
      <router-link to="/books">
        Library
      </router-link>
      <router-link to="/about">
        About
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Navigation',
  methods: {
    async goHome() {
      await this.$router.push({ name: 'Home' })
    }
  }
}
</script>

<style>
#nav {
  @apply flex flex-row items-center p-1 mb-8 bg-gray-800;
}

#nav .title {
  @apply cursor-pointer text-3xl font-semibold text-blue-500;
}

#nav .items {
  @apply flex-grow text-center;
}

#nav a {
  @apply text-gray-100 no-underline ml-4;
}

#nav a.router-link-exact-active {
  @apply text-blue-300;
}
</style>