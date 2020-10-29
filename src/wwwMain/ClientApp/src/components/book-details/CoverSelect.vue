<template>
  <field-details v-if="showIt" title="Cover Data" @close="coverDetailsClose">
    <div v-if="raw.isbn !== ''" class="grid grid-cols-3 gap-x-1">
      <div class="flex flex-row">
        <img :src="raw.bookFinder.coverImageUrl" alt="Book Cover" class="rounded-t" style="height: 10rem" @click="selectCover(1)">
        <div class="ml-3" style="height: 10rem; line-height: 10rem">
          <font-awesome-icon v-if="coverSeln == 1" :icon="[ 'fas', 'check-circle' ]" class="text-2xl text-green-400" />
        </div>
      </div>
      <div class="flex flex-row">
        <img v-if="raw.isbnDb.coverImageUrl != null" :src="raw.isbnDb.coverImageUrl" alt="Book Cover" class="rounded-t" style="height: 10rem"
             @click="selectCover(2)"
        >
        <div class="ml-3" style="height: 10rem; line-height: 10rem">
          <font-awesome-icon v-if="coverSeln == 2" :icon="[ 'fas', 'check-circle' ]" class="text-2xl text-green-400" />
        </div>
      </div>
      <div class="flex flex-row">
        <img v-if="raw.openLibrary.coverImageUrl != null" :src="raw.openLibrary.coverImageUrl" alt="Book Cover" class="rounded-t" style="height: 10rem"
             @click="selectCover(3)"
        >
        <div class="ml-3" style="height: 10rem; line-height: 10rem">
          <font-awesome-icon v-if="coverSeln == 3" :icon="[ 'fas', 'check-circle' ]" class="text-2xl text-green-400" />
        </div>
      </div>
      <div>
        Book Finder
      </div>
      <div>
        ISBN Db
      </div>
      <div>
        Open Library
      </div>
    </div>
  </field-details>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from '@vue/composition-api';
import FieldDetails from '@/components/book-details/FieldDetails.vue';
import RawBookData from '../../common/types/raw-book-data';
import BookStore from '../../common/store/book-store';

export default defineComponent({
  name: 'cover-select',
  components: { FieldDetails },
  props: {
    forIsbn: {
      required: true,
      type: String,
    },
    showIt: {
      required: true,
      showIt: Boolean,
    },
  },
  setup(props) {
    const raw = ref(new RawBookData());
    const coverSeln = ref(1);

    const refresh = async () => {
      raw.value = await BookStore.getBookRawData(props.forIsbn);
      coverSeln.value = 0;
    };

    watch(() => props.showIt, async (newval) => {
      if (newval) {
        await refresh();
      }
    });

    function coverDetailsClose(doUpdate: boolean) {
      this.$emit('close', true);
      if (doUpdate) {
        let newCover = '';
        switch (coverSeln.value) {
          case 1:
            newCover = raw.value.bookFinder.coverImageUrl;
            break;
          case 2:
            newCover = raw.value.isbnDb.coverImageUrl;
            break;
          case 3:
            newCover = raw.value.openLibrary.coverImageUrl;
            break;
          default:
            newCover = '';
            break;
        }
        // now update book
        console.log(`New cover = ${newCover}`);
        this.$emit('updated', true);
      }
    }

    function selectCover(num: number) {
      coverSeln.value = num;
    }

    return {
      raw,
      coverDetailsClose,
      coverSeln,
      selectCover,
    };
  },
});
</script>
