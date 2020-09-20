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
      />
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted } from '@vue/composition-api';
import BookCover from '@/components/BookCover.vue';
import BookStore from '../common/store/book-store';

export default defineComponent({
  name: 'Home',
  components: {
    BookCover,
  },
  setup() {
    const bookList = BookStore.getBooks.value;

    onMounted(async () => {
      await BookStore.loadBooks();
    });

    return {
      bookList,
    };
  },
});
</script>
