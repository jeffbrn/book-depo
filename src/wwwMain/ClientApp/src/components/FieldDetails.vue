<template>
  <div v-if="showIt" class="bg-white border fixed bottom-0 w-full pt-2 px-4" style="max-width: 1024px; height: 16rem">
    <div class="grid grid-cols-8 h-full">
      <div class="col-span-8">
        <button class="float-right border-0 hover:text-gray-600 relative" style="-.5rem" @click="close">
          <font-awesome-icon :icon="[ 'fas', 'times-circle' ]" class="text-sm" />
        </button>
        <h2>{{ title }}</h2>
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
    const primaryBtn = ref([...DefaultStyles.primaryButton, 'm-1']);
    const secondaryBtn = ref([...DefaultStyles.secondaryButton, 'm-1']);

    onBeforeUpdate(() => {
      // console.log('onBeforeUpdate');
    });

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
