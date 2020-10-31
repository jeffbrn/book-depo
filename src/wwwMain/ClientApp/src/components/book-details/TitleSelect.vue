<template>
  <field-details v-if="showIt" title="Title Data" @close="coverDetailsClose">
    <div v-if="raw.isbn !== ''" class="flex flex-col">
      <div>
        <span :class="labelStyle">Book Finder</span>
        <input type="text" v-model="raw.bookFinder.title" :class="inputStyle" readonly style="height: 0.75rem; font-size: 50%; top: -0.75rem">
      </div>
      <div>
        <span :class="labelStyle">ISBN Db</span>
        <input type="text" v-model="raw.isbnDb.title" :class="inputStyle" readonly style="height: 0.75rem; font-size: 50%; top: -0.75rem">
      </div>
      <div>
        <span :class="labelStyle">Open Library</span>
        <input type="text" v-model="raw.openLibrary.title" :class="inputStyle" readonly style="height: 0.75rem; font-size: 50%; top: -0.75rem">
      </div>
      <div>
        <span :class="labelStyle">User Edit</span>
        <input type="text" v-model="userTitle" :class="inputStyle" readonly style="height: 0.75rem; font-size: 50%; top: -0.75rem">
      </div>
    </div>
  </field-details>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from '@vue/composition-api';
import FieldDetails from '@/components/book-details/FieldDetails.vue';
import RawBookData from '../../common/types/raw-book-data';
import BookStore from '../../common/store/book-store';
import DefaultStyles from '../../common/styles/classes';

export default defineComponent({
  name: 'title-select',
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
  setup(props, { emit }) {
    const raw = ref(new RawBookData());
    const coverSeln = ref(1);
    const userTitle = ref('');

    const labelStyle = ref([...DefaultStyles.labelStyle]);
    const inputStyle = ref([...DefaultStyles.inputStyle, 'relative']);

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
      emit('close', true);
      if (doUpdate) {
        let newCover = '';
        switch (coverSeln.value) {
          case 1:
            newCover = raw.value.bookFinder?.coverImageUrl ?? '';
            break;
          case 2:
            newCover = raw.value.isbnDb?.coverImageUrl ?? '';
            break;
          case 3:
            newCover = raw.value.openLibrary?.coverImageUrl ?? '';
            break;
          default:
            newCover = '';
            break;
        }
        // now update book
        console.log(`New cover = ${newCover}`);
        emit('updated', true);
      }
    }

    function selectCover(num: number) {
      coverSeln.value = num;
    }

    return {
      raw,
      coverDetailsClose,
      coverSeln,
      userTitle,
      selectCover,
      labelStyle,
      inputStyle,
    };
  },
});
</script>
