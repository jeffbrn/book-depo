<template>
  <div id="app" class="bg-gray-200 font-sans leading-normal tracking-normal">
    <page-header title="The Book Depository" sub-title="for the Brown family" />

    <!--Container-->
    <div class="container px-4 max-w-6xl mx-auto -mt-20">
      <div class="mx-0 sm:mx-6">
        <page-nav />

        <router-view />
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';
import PageHeader from './components/PageHeader.vue';
import PageNav from './components/PageNav.vue';

export default defineComponent({
  name: 'App',
  components: {
    PageHeader,
    PageNav,
  },

  setup() {
    return {};
  },
});
</script>
