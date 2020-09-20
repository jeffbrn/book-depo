import Vue from 'vue';
import VueCompositionApi, { computed, ref } from '@vue/composition-api';
import ApiService from '@/common/services/api-service';
import BookSumm from '@/common/types/booksumm';

Vue.use(VueCompositionApi);

const api = ApiService.bookApi();
const list: BookSumm[] = [];
const map: Map<string, BookSumm> = new Map();

const listState = ref(list);
const mapState = ref(map);

function setBooks(booklist: BookSumm[]) {
  listState.value = booklist;
  booklist.forEach((element) => {
    mapState.value.set(element.id, element);
  });
}

async function loadBooks() {
  const response = await api.getAll();
  setBooks(response.data);
}

const getBooks = computed(() => listState);

const BookStore = {
  setBooks,
  loadBooks,
  getBooks,
};

export default BookStore;
