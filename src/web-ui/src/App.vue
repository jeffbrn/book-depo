<template>
  <div>
    <navigation />
    <router-view id="content" />
  </div>
</template>

<script>
import Navigation from '@/components/Navigation.vue'

export default {
  name: 'Application',
  components: {
    Navigation
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  @apply text-gray-600;
}

#content {
  @apply container mx-auto;
}
</style>