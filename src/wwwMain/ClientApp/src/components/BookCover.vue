<template>
  <div>
    <div class="flex-1 bg-white rounded-t rounded-b-none overflow-hidden shadow-lg">
      <a href="#" class="flex flex-wrap no-underline hover:no-underline">
        <img :src="bookCoverSrc()" alt="Book Cover" class="h-64 w-full rounded-t" @click="details">
      </a>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'BookCover',
  props: {
    bookId: {
      required: true,
      bookId: String,
    },
  },
  setup(props, ctx) {
    function bookCoverSrc() {
      return (typeof props.bookId === 'undefined') ? '' : `/api/book/${props.bookId}/cover`;
    }

    function details(this: any) {
      ctx.root.$router.push({ path: `details/${props.bookId}` });
    }

    return {
      bookCoverSrc,
      details,
    };
  },
});
</script>
