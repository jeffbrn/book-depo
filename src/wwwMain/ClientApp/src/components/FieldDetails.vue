<template>
  <div v-if="showIt" class="bg-white border fixed bottom-0 w-full pt-2 px-4" style="max-width: 1024px; height: 16rem">
    <div class="grid grid-cols-8">
      <div class="col-span-8">
        <button class="float-right border-0 hover:text-gray-600" @click="close">
          <font-awesome-icon :icon="[ 'fas', 'times-circle' ]" class="text-sm" />
        </button>
        <h2>{{ title }}</h2>
      </div>
      <div class="col-span-7">
        <slot />
      </div>
      <div class="pt-10">
        <div class="flex flex-col-reverse">
          <button :class="primaryBtn">
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
import { defineComponent, onBeforeUpdate, ref } from '@vue/composition-api';
import DefaultStyles from '../common/styles/classes';

export default defineComponent({
  name: 'FieldDetails',
  props: {
    showIt: {
      required: true,
      showIt: Boolean,
    },
    title: {
      required: true,
      title: String,
    },
  },
  setup() {
    const primaryBtn = ref([...DefaultStyles.primaryButton, 'm-3']);
    const secondaryBtn = ref([...DefaultStyles.secondaryButton, 'm-3']);

    onBeforeUpdate(() => {
      console.log('onBeforeUpdate');
    });

    function close(this: any) {
      this.$emit('close');
    }

    function update(this: any) {
      this.$emit('update');
    }

    return {
      close,
      primaryBtn,
      secondaryBtn,
    };
  },
});
</script>
