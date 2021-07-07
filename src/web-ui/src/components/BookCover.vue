<template>
  <div class="book-cover">
    <img
      :src="bookCoverImgSrc"
      alt="Book Cover"
    >
  </div>
</template>

<script>
export default {
  name: 'BookCover',
  props: {
    bookId: {
      type: String,
      required: true,
      default: ''
    }
  },
  computed: {
    bookCoverImgSrc() {
      return (typeof this.bookId === 'undefined') ? '' : `https://localhost:7001/api/book/${this.bookId}/cover`;
    }
  }
}
</script>

<style>
.book-cover {
  @apply cursor-pointer;
}

.book-cover a {
  @apply no-underline hover:no-underline;
}

.book-cover img {
  @apply h-64 w-full rounded-lg;
}
</style>