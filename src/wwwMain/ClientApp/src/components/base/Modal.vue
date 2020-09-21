<template>
  <transition name="fade">
    <div v-if="showing" class="fixed inset-0 w-full h-screen flex items-center justify-center bg-smoke-60" @keydown.esc="close" tabindex="0">
      <div style="max-height: 90vh;" class="relative w-full max-w-2xl overflow-auto bg-white shadow-lg rounded-lg p-8">
        <button
          id="modal_close"
          aria-label="close"
          class="absolute top-0 right-0 text-xl text-gray-500 my-2 mx-4 focus:outline-none"
          @click.prevent="close"
          ref="closeBtn"
        >
          ×
        </button>
        <slot />
      </div>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from '@vue/composition-api';

export default defineComponent({
  name: 'Modal',
  props: {
    showing: {
      required: true,
      type: Boolean,
    },
  },
  setup(props) {
    const closeBtn = ref({} as HTMLButtonElement); // populated from DOM after setup using 'ref' attribute

    function close(this: any) {
      this.$emit('close');
    }

    // watch the showing property so that when modal is visible scrolling is stopped on the background and to autofouce the close button
    watch(() => props.showing, (newval) => {
      if (newval) {
        document.querySelector('body')!.classList.add('overflow-hidden');
        const btn = closeBtn.value as HTMLButtonElement;
        btn.focus();
      } else {
        document.querySelector('body')!.classList.remove('overflow-hidden');
      }
    });

    return {
      close,
      closeBtn,
    };
  },
});
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: all 0.6s;
}
.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
