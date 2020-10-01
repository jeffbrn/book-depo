<template>
  <div class="bg-gray-200 w-full text-xl md:text-2xl text-gray-800 leading-normal rounded-t">
    <div class="w-full p-6 flex flex-col flex-shrink">
      <p>Book Details</p>
      <p>Id = {{ bookId }}</p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'Home',
  setup(props, ctx) {
    const route = ctx.root.$route;

    const { bookId } = route.params;

    return {
      bookId,
    };
  },
});
</script>
