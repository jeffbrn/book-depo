<template>
  <div class="bg-white border fixed bottom-0 w-full pt-2 px-4" style="max-width: 1024px; height: 16rem">
    <div class="grid grid-cols-8 h-full">
      <div class="col-span-8">
        <h2 class="rounded mb-2 px-2 font-bold text-white bg-blue-300">
          {{ title }}
        </h2>
      </div>
      <div class="col-span-7">
        <slot />
      </div>
      <div>
        <div class="flex flex-col">
          <button :class="primaryBtn" @click="update">
            Update
          </button>
          <button :class="secondaryBtn" @click="close">
            Cancel
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from '@vue/composition-api';
import DefaultStyles from '../../common/styles/classes';

export default defineComponent({
  name: 'FieldDetails',
  props: {
    title: {
      required: true,
      title: String,
    },
  },
  setup() {
    const primaryBtn = ref([...DefaultStyles.primaryButton, 'm-1']);
    const secondaryBtn = ref([...DefaultStyles.secondaryButton, 'm-1']);

    function close(this: any) {
      this.$emit('close', false);
    }

    function update(this: any) {
      this.$emit('close', true);
    }

    return {
      close,
      update,
      primaryBtn,
      secondaryBtn,
    };
  },
});
</script>
