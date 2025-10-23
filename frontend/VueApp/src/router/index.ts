import LoanApplicationsDirectory from '@/components/directories/LoanApplicationsDirectory.vue';
import LoanApplicationCreateForm from '@/components/forms/create/LoanApplicationCreateForm.vue';
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  { path: '/', redirect: '/loans' },
  { path: '/loans', component: LoanApplicationsDirectory },
  { path: '/loans/create', component: LoanApplicationCreateForm }
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
