<template>
  <div class="bg-gray-200 w-full text-xl md:text-2xl text-gray-800 leading-normal rounded-t">
    <div v-if="!loading" class="w-full p-6 flex flex-col flex-shrink">
      <div class="mt-10">
        <div class="text-sm">
          <router-link to="/" class="inline-block text-blue-700 no-underline hover:text-blue-400 hover:underline">
            Books
          </router-link>
          <font-awesome-icon :icon="[ 'fas', 'angle-double-right' ]" class="ml-4 mr-2 text-gray-500" />
          {{ details.isbn }}
        </div>
        <div class="text-center">
          <h2>Book Details</h2>
        </div>
        <div class="grid grid-cols-4 gap-4">
          <div class="p-5">
            <div class="flex flex-col">
              <img :src="bookCoverSrc()" alt="Book Cover" class="h-64 w-full rounded-t" @click="details">
              <div class="w-fill content-center">
                <button class="border rounded shadow w-10 bg-teal-400 text-white focus:outline-none hover:bg-teal-500 focus:bg-teal-500" @click="showFieldDetails">
                  <font-awesome-icon :icon="[ 'fas', 'pencil-alt' ]" />
                </button>
              </div>
            </div>
          </div>
          <div class="col-span-3">
            <div class="flex flex-col">
              <div :class="labelStyle">
                Title
              </div>
              <div>
                <input type="text" v-model="details.title" :class="inputStyle" readonly>
              </div>
            </div>
            <div class="flex flex-col">
              <div class="grid grid-cols-2 gap-4">
                <div>
                  <div :class="labelStyle">
                    Author
                  </div>
                  <div>
                    <input type="text" v-model="details.author" :class="inputStyle" readonly>
                  </div>
                </div>
                <div>
                  <div :class="labelStyle">
                    Publisher
                  </div>
                  <div>
                    <input type="text" v-model="details.publisher" :class="inputStyle" readonly>
                  </div>
                </div>
              </div>
            </div>
            <div class="flex flex-col">
              <div class="grid grid-cols-2 gap-4">
                <div>
                  <div :class="labelStyle">
                    # Pages
                  </div>
                  <div>
                    <input type="number" v-model="details.numPages" :class="inputNumStyle" readonly>
                  </div>
                </div>
                <div>
                  <div :class="labelStyle">
                    Published On
                  </div>
                  <div>
                    <input type="text" v-model="publishedDate" :class="inputStyle" readonly>
                  </div>
                </div>
              </div>
            </div>
            <div class="flex flex-col">
              <div :class="labelStyle">
                Description
              </div>
              <div>
                <textarea v-model="details.description" :class="inputStyle" readonly rows="6" />
              </div>
            </div>
          </div>
        </div>
        <field-details :show-it="detailsShow" title="Cover Data" @close="closeFieldDetails">
          field details!
        </field-details>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from '@vue/composition-api';
import FieldDetails from '@/components/FieldDetails.vue';
import BookDetails from '../common/types/bookdetails';
import BookStore from '../common/store/book-store';

export default defineComponent({
  name: 'Home',
  components: { FieldDetails },
  setup(props, ctx) {
    const route = ctx.root.$route;
    const details = ref(new BookDetails());
    const loading = ref(true);
    const labelStyle = ref(['text-xs', 'text-blue-400', 'mt-6']);
    const inputStyle = ref(['w-full', 'border', 'rounded', 'py-2', 'px-4', 'text-sm', 'bg-gray-100', 'text-gray-700', 'border-gray-300']);
    const inputNumStyle = ref(['w-32', 'border', 'rounded', 'py-2', 'px-4', 'text-sm', 'bg-gray-100', 'text-gray-700', 'border-gray-300']);
    const publishedDate = ref('');

    const detailsShow = ref(false);

    const { bookId } = route.params;

    onMounted(async () => {
      details.value = await BookStore.getBook(bookId);
      if (details.value.publishedOn === null) {
        publishedDate.value = details.value.publishedOnRaw;
      } else {
        publishedDate.value = ctx.root.$options.filters.dateFormatter(details.value.publishedOn);
      }
      loading.value = false;
    });

    function bookCoverSrc() {
      return `/api/book/${bookId}/cover`;
    }

    function showFieldDetails() {
      detailsShow.value = true;
    }

    function closeFieldDetails() {
      detailsShow.value = false;
    }

    return {
      details,
      loading,
      bookCoverSrc,
      labelStyle,
      inputStyle,
      inputNumStyle,
      publishedDate,
      detailsShow,
      showFieldDetails,
      closeFieldDetails,
    };
  },
});
</script>
