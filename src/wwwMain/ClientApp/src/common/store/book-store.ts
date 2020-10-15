import Vue from 'vue';
import VueCompositionApi, { computed, ref } from '@vue/composition-api';
import ApiService from '@/common/services/api-service';
import BookSumm from '@/common/types/book-summ';
import BookDetails from '@/common/types/book-details';
import RawBookData from '@/common/types/raw-book-data';

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

async function getBook(id: string): Promise<BookDetails> {
  const response = await api.getDetails(id);
  return response.data;
}

async function getBookRawData(isbn: string): Promise<RawBookData> {
  const response = await api.getRawData(isbn);
  return response.data;
}

const BookStore = {
  setBooks,
  loadBooks,
  getBooks,
  getBook,
  getBookRawData,
};

export default BookStore;
