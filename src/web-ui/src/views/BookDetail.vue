<template>
  <div class="form-detail">
    <h2>Book Details</h2>
    <div
      v-if="!loading"
      class="grid grid-cols-4 gap-4"
    >
      <div class="col-span-4">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            Title
          </dt>
          <dd>{{ details.title }}</dd>
        </dl>
      </div>
      <div class="col-span-2">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            Author
          </dt>
          <dd>{{ details.author }}</dd>
        </dl>
      </div>
      <div class="col-span-2">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            Publisher
          </dt>
          <dd>{{ details.publisher }}</dd>
        </dl>
      </div>
      <div class="col-span-2">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            Published On
          </dt>
          <dd v-if="details.publishedOn">
            {{ $filters.dateFormatter1(details.publishedOn) }}
          </dd>
          <dd
            v-else
            class="text-orange-500"
          >
            {{ details.publishedOnRaw }}
          </dd>
        </dl>
      </div>
      <div class="col-span-1">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            # Pages
          </dt>
          <dd>{{ details.numPages }}</dd>
        </dl>
      </div>
      <div class="col-span-1">
        <dl>
          <dt class="text-blue-400 text-xs uppercase font-bold">
            ISBN
          </dt>
          <dd>{{ details.isbn }}</dd>
        </dl>
      </div>
      <div class="col-span-4">
        <p class="text-blue-400 text-xs uppercase font-bold">
          Description
        </p>
        <textarea
          v-model="details.description"
          class="w-full text-xs"
          rows="10"
          disabled
          readonly
        />
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from '@/services/api/api-services';

export default {
  name: 'BookDetail',
  data() {
    return {
      api: null,
      loading: true,
      details: null
    }
  },
  methods: {
    async refresh() {
      try {
        this.loading = true;
        const { data } = await this.api.getDetails(this.$route.params.bid);
        this.details = data;
        this.loading = false;
      } catch(error) {
        console.log(error);
      }
    }
  },
/*  created() {
    this.$watch()
  },
*/  mounted() {
    this.api = ApiService.bookApi();
    this.refresh();
  }
}
</script>