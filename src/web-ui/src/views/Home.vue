<template>
  <div>
    <img
      class="m-auto"
      alt="Vue logo"
      src="../assets/logo.png"
    >
    <HelloWorld msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import ApiService from '@/services/api/api-services';

export default {
  name: 'Home',
  components: {
    HelloWorld
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
    }
  },
  mounted() {
    this.api = ApiService.bookApi();
    this.refresh();
  }
}
</script>
