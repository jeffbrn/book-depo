<template>
  <div
    class="w-full m-0 p-0 bg-cover bg-top h-64"
    style="background-image:url('/header.jpg'); height=60vh; max-height:460px"
  >
    <div class="container max-w-4xl mx-auto pt-16 text-center break-normal">
      <p class="text-white font-extrabold text-3xl md:text-5xl">
        {{ title }}
      </p>
      <p class="text-xl md:text-2xl text-gray-500">
        {{ subTitle }}
      </p>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from '@vue/composition-api';

export default defineComponent({
  name: 'PageHeader',
  props: {
    title: {
      type: String,
      required: true,
    },
    subTitle: {
      type: String,
      default: '',
    },
  },

  setup() {
    return {};
  },
});
</script>
