<template>
  <nav class="mt-0 w-full">
    <div class="container mx-auto flex items-center">
      <div class="flex w-1/2 pl-4 text-sm">
        <ul class="list-reset flex justify-between flex-1 md:flex-none items-center">
          <li class="mr-2">
            <router-link to="/" class="inline-block py-2 px-2 text-white no-underline hover:underline">
              Home
            </router-link>
          </li>
          <li class="mr-2">
            <router-link to="/about" class="inline-block text-gray-600 no-underline hover:text-gray-200 hover:underline py-2 px-2">
              About
            </router-link>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'PageNav',

  setup() {
    return {};
  },
});
</script>
