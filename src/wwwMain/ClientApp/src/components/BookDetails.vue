<template>
  <modal :showing="ShowDetails" @close="CloseDlg">
    <div class="prose">
      <h2>Book Details</h2>
      <div v-if="!loading" class="grid grid-cols-4 gap-4">
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
              {{ details.publishedOn | dateFormatter }}
            </dd>
            <dd v-else class="text-orange-500">
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
          <textarea v-model="details.description" class="w-full text-xs" rows="10" disabled readonly />
        </div>
      </div>
      <button
        class="bg-blue-500 text-white px-4 py-1 text-sm uppercase tracking-wide font-bold rounded hover:bg-blue-700"
        @click="CloseDlg"
      >
        Close
      </button>
    </div>
  </modal>
</template>
<script lang="ts">
import {
  defineComponent, ref, onBeforeUpdate,
} from '@vue/composition-api';
import BookDetails from '../common/types/bookdetails';
import BookStore from '../common/store/book-store';
import Modal from './base/Modal.vue';

export default defineComponent({
  name: 'BookDetails',
  components: {
    Modal,
  },
  props: {
    ShowDetails: {
      required: true,
      type: Boolean,
    },
    BookId: {
      required: true,
      type: String,
    },
  },
  setup(props) {
    const details = ref(new BookDetails());
    const loading = ref(true);

    function CloseDlg(this: any) {
      loading.value = true;
      this.$emit('close');
    }

    onBeforeUpdate(async () => {
      details.value = await BookStore.getBook(props.BookId);
      loading.value = false;
    });

    return {
      CloseDlg,
      details,
      loading,
    };
  },
});
</script>
