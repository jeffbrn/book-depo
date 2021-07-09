<template>
  <div class="text-center">
    <h1>Brown Family Book Catalog</h1>
    <div>
      <p>This website tracks and catalogs the Brown family library.</p>
    </div>
    <div
      v-if="!loading"
      class="text-left"
    >
      <h2 class="mt-12 text-blue-800">
        Library Statistics
      </h2>
      <p>Number of books in catalog: <span class="font-bold">{{ stats.numBooks }}</span></p>
      <h2 class="mt-5 text-blue-800">
        Latest Additions
      </h2>
      <div class="grid grid-cols-2 gap-4 mt-6">
        <div class="font-bold">
          Book Title
        </div>
        <div class="font-bold">
          Added on
        </div>
        <!-- eslint-disable -->
        <template
          v-for="b in stats.latestUploaded"
          :key="b.id"
        >
          <router-link
            :to="{ name: 'BookDetail', params: {bid: b.id } }"
          >
            {{ b.title }}
          </router-link>
          <div>
            {{ $filters.datetimeFormatter1(b.addedOn) }}
          </div>
        </template>
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from '@/services/api/api-services';

export default {
  name: 'Home',
  data() {
    return {
      api: null,
      loading: true,
      stats: null
    }
  },
  methods: {
    async refresh() {
      try {
        this.loading = true;
        this.books = [];
        const { data } = await this.api.getStats();
        this.stats = data;
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
