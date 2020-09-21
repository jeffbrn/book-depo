<template>
  <div class="bg-gray-200 w-full text-xl md:text-2xl text-gray-800 leading-normal rounded-t">
    <!--Posts Container-->
    <div v-if="bookList.length === 0" class="w-full p-6 flex flex-col flex-shrink">
      <p>Loading ...</p>
    </div>
    <div v-if="bookList.length !== 0" class="flex flex-wrap justify-start pt-12 -mx-6">
      <book-cover class="w-1/5 p-6 flex flex-col flex-shrink"
                  v-for="b in bookList"
                  :book-id="b.id"
                  :key="b.id"
                  @details="showit"
      />
    </div>
    <book-details :show-details="showModal" @close="showModal = false" :book-id="selnId" />
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from '@vue/composition-api';
import BookCover from '@/components/BookCover.vue';
import BookDetails from '@/components/BookDetails.vue';
import BookStore from '../common/store/book-store';

export default defineComponent({
  name: 'Home',
  components: {
    BookCover,
    BookDetails,
  },
  setup() {
    const bookList = BookStore.getBooks.value;
    const showModal = ref(false);
    const selnId = ref('');

    onMounted(async () => {
      await BookStore.loadBooks();
    });

    function showit(bid: string) {
      selnId.value = bid;
      showModal.value = true;
    }

    return {
      bookList,
      showit,
      showModal,
      selnId,
    };
  },
});
</script>
