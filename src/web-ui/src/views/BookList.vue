<template>
  <div class="book-list">
    <div v-if="loading">
      <p>Loading ...</p>
    </div>
    <div
      v-if="!loading"
      class="covers"
    >
      <book-cover
        class="cover-list"
        v-for="b in books"
        :key="b.id"
        :book-id="b.id"
        @click="gotoDetails(b.id)"
      />
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import BookCover from '@/common/components/BookCover.vue';
import ApiService from '@/services/api/api-services';

export default {
  name: 'BookList',
  components: {
    BookCover
  },
  data() {
    return {
      api: null,
      loading: true,
      books: []
    }
  },
  methods: {
    async refresh() {
      try {
        this.loading = true;
        const { data } = await this.api.getAll();
        this.books = data;
        this.loading = false;
      } catch(error) {
        console.log(error);
      }
    },
    async gotoDetails(id) {
      await this.$router.push({ name: 'BookDetail', params: { bid: id } });
    }
  },
  mounted() {
    this.api = ApiService.bookApi();
    this.refresh();
  }
}
</script>

<style>
.book-list {
  @apply bg-white w-full text-xl md:text-2xl text-gray-800 leading-normal rounded-t;
}

.book-list .empty-list {
  @apply w-full p-6 flex flex-col flex-shrink;
}

.book-list .covers {
  @apply flex flex-row flex-wrap justify-start pt-12;
}

.book-list .cover-list {
  @apply w-1/8 mx-3 p-0 shadow-lg;
}
</style>
