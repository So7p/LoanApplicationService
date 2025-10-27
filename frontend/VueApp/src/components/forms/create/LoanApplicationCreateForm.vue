<template>
    <div class="form-page">
        <el-card class="form-card">
            <h2 class="form-title">Создание новой заявки</h2>
            <el-form
                ref="formRef"
                :model="formData"
                :rules="validationRules"
                label-position="top"
                class="loan-form"
            >
                <el-form-item label="Сумма (р.)" prop="amount">
                    <el-input-number
                    v-model="formData.amount"
                    :min="1"
                    :max="100000000"
                    :step="1000"
                    placeholder="Сумма займа"
                    class="input-full"
                    />
                </el-form-item>

                <el-form-item label="Срок (мес.)" prop="termValue">
                    <el-input-number
                    v-model="formData.termValue"
                    :min="1"
                    :max="36"
                    placeholder="Срок займа"
                    class="input-full"
                    />
                </el-form-item>

                <el-form-item label="Процентная ставка (%)" prop="interestValue">
                    <el-input-number
                    v-model="formData.interestValue"
                    :min="0.1"
                    :max="50"
                    :step="0.1"
                    placeholder="Процентная ставка"
                    class="input-full"
                    />
                </el-form-item>

                <el-form-item>
                    <div class="form-buttons">
                        <el-button type="success" @click="submitForm">Создать заявку</el-button>
                        <router-link to="/">
                            <el-button>Назад</el-button>
                        </router-link>
                    </div>
                </el-form-item>
            </el-form>
        </el-card>

        <el-dialog v-model="dialogVisible" width="400px" align-center>
            <template #header>
                <h2 style="text-align: center;">Заявка добавлена!</h2>
            </template>
            <template #footer>
                <router-link to="/">
                    <el-button type="primary">Вернуться к списку заявок</el-button>
                </router-link>
            </template>
        </el-dialog>
    </div>
</template>

<script setup lang="ts">
    import { ref } from 'vue'
    import { ElMessage } from 'element-plus'
    import { createLoanApplication } from '@/api/services/dataSource/LoanApplicationsDataSourceService';
    import type { ILoanApplication } from '@/classes/POCOs/LoanApplication';

    const formData = ref<Pick<ILoanApplication, 'amount' | 'termValue' | 'interestValue'>>({} as Pick<ILoanApplication, 'amount' | 'termValue' | 'interestValue'>)

    const formRef = ref();

    const validationRules = {
        amount: [
            { required: true, message: 'Сумма займа', trigger: 'blur' },
            { type: 'number', min: 1, max: 100000000, message: 'От 1 до 100 000 000' }
        ],
        termValue: [
            { required: true, message: 'Срок займа', trigger: 'blur' },
            { type: 'number', min: 1, max: 36, message: 'От 1 до 36 мес.' }
        ],
        interestValue: [
            { required: true, message: 'Процент займа', trigger: 'blur' },
            { type: 'number', min: 0.1, max: 50, message: 'От 0.1 до 50%' }
        ]
    };

    const dialogVisible = ref(false);

    const submitForm = async () => {
        formRef.value.validate(async (valid: boolean) => {
            if (valid) {
                try {
                    await createLoanApplication(formData.value);
                    dialogVisible.value = true 
                } catch {
                    ElMessage.error('Ошибка при создании заявки')
                }
            } else {
                ElMessage.warning('Проверьте данные формы')
            }
        })
    };
</script>

<style scoped>
    .form-page {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background: #f5f5f5;
    }

    .form-card {
        width: 400px;
        padding: 20px;
    }

    .form-title {
        text-align: center;
        margin-bottom: 20px;
    }

    .input-full {
        width: 100%;
    }

    ::v-deep(.el-dialog__header.show-close),
    ::v-deep(.el-dialog__footer) {
        text-align: center;
        padding: 0;
    }

    .form-buttons {
        display: flex;  
        justify-content: space-between;
        width: 100%;
    }
</style>