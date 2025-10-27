<template>
    <el-container style="min-height: 100vh; display: flex; justify-content: center; align-items: center; padding: 2vh 0">
        <el-container style="width: 90%;">

            <el-header style="padding: 0% 5%">
                <el-row gutter="10" style="width: 100%; justify-content: space-between;">
                    <el-form
                        :model="filters"
                        :rules="filterValidationRules"
                        ref="filtersForm"
                        label-width="0"
                        inline
                        style="display: flex; align-items: center; justify-content: space-between; width: 80%; gap: 2vh"
                    >
                        
                        <el-col :span="5" class="filter-item">
                            <span>Статус:</span>
                            <el-select v-model="filters.status" placeholder="Статус" clearable style="width: 100%;">
                                <el-option label="Все" :value="null" />
                                <el-option label="Опубликована" :value="LoanStatusEnum.Published" />
                                <el-option label="Снята с публикации" :value="LoanStatusEnum.Unpublished" />
                            </el-select>
                        </el-col>

                        <el-col :span="8" class="filter-item">
                            <span>Сумма:</span>

                            <el-input-number
                                v-model="filters.minAmount"
                                :min="1"
                                :max="100000000"
                                placeholder="От"
                                style="width: 45%;"
                            />
                            <span>&mdash;</span>
                            <el-input-number
                                v-model="filters.maxAmount"
                                :min="1"
                                :max="100000000"
                                placeholder="До"
                                style="width: 45%;"
                            />
                        </el-col>

                        <el-col :span="8" class="filter-item">
                            <span>Срок:</span>

                            <el-input-number
                                v-model="filters.minTermValue"
                                :min="1"
                                :max="36"
                                placeholder="От"
                                style="width: 45%;"
                            />
                            <span>&mdash;</span>
                            <el-input-number
                                v-model="filters.maxTermValue"
                                :min="1"
                                :max="36"
                                placeholder="До"
                                style="width: 45%;"
                            />
                        </el-col>

                        <el-col :span="2">
                            <el-button type="primary" style="width: 100%;" @click="applyFilters">
                                Применить
                            </el-button>
                        </el-col>
                    </el-form>
                    
                    <el-col :span="2" style="text-align: right; padding: 0 !important;">
                        <router-link to="/loans/create">
                            <el-button type="success">Создать заявку</el-button>
                        </router-link>
                    </el-col>
                </el-row>
            </el-header>

            <el-main style="padding: 0 5%;">
                <el-table
                    v-loading="loading"
                    :data="tableData"
                    style="width: 100%"
                    stripe
                    border
                >
                    <el-table-column sortable prop="number" label="Номер" width="400" />
                    <el-table-column sortable prop="amount" label="Сумма (р.)" width="150" />
                    <el-table-column sortable prop="termValue" label="Срок (мес.)" width="150" />
                    <el-table-column sortable prop="interestValue" label="Процент (%)" width="150" />
                    <el-table-column sortable prop="createdAt" label="Создана" width="220" />
                    <el-table-column sortable prop="modifiedAt" label="Изменена" width="220" />
                    <el-table-column sortable prop="statusText" label="Статус" width="220" />

                    <el-table-column label="Действия" width="190">
                        <template #default="scope">
                            <el-button
                                size="small"
                                type="danger"
                                v-if="scope.row.statusText === 'Опубликована'"
                                @click="editStatus(scope.row)"
                            >
                                Снять с публикации
                            </el-button>
                            <el-button
                                size="small"
                                type="success"
                                v-else
                                @click="editStatus(scope.row)"
                            >
                                Опубликовать
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </el-main>

        </el-container>
    </el-container>
</template>

<script setup lang="ts">
    import type { IGetLoanApplicationsParams } from '@/api/services/controllerServices/LoanApplicationsControllerService';
    import { editLoanApplicationStatus, getLoanApplications, LoanApplicationsDirectiryGridDataItem } from '@/api/services/dataSource/LoanApplicationsDataSourceService';
    import { LoanStatusEnum } from '@/classes/enums/LoanStatusEnum';
    import { getLoanStatusLabel } from '@/helpers/LoanStatusEnumHelper';
    import { ElMessage, type FormItemRule } from 'element-plus';
    import { onMounted, ref } from 'vue';

    const tableData = ref<LoanApplicationsDirectiryGridDataItem[]>([]);
    const loading = ref(false);

    const filters = ref<{
        status: LoanStatusEnum | null,
        minAmount: number | null,
        maxAmount: number | null,
        minTermValue: number | null,
        maxTermValue: number | null
    }>({
        status: null,
        minAmount: null,
        maxAmount: null,
        minTermValue: null,
        maxTermValue: null
    });

    const filtersForm = ref();

    const filterValidationRules = {
        minAmount: [
            { 
                type: 'number', 
                min: 1, 
                max: 100000000, 
                message: 'От 1 до 100 000 000', 
                trigger: 'blur' 
            },
            { 
            validator: (rule: FormItemRule, value: number, callback: (error?: Error) => void) => {
                if (value != null && filters.value.maxAmount != null && value > filters.value.maxAmount) {
                    callback(new Error('Минимальная сумма не может быть больше максимальной'));
                } else {
                    callback();
                }
            }, 
            trigger: 'blur' 
            }
        ],
        maxAmount: [
            { 
                type: 'number', 
                min: 1, 
                max: 100000000, 
                message: 'От 1 до 100 000 000', 
                trigger: 'blur' 
            },
            { 
            validator: (rule: FormItemRule, value: number, callback: (error?: Error) => void) => {
                if (value != null && filters.value.minAmount != null && value < filters.value.minAmount) {
                    callback(new Error('Максимальная сумма не может быть меньше минимальной'));
                } else {
                    callback();
                }
            }, 
            trigger: 'blur' 
            }
        ],
        minTermValue: [
            { 
                type: 'number', 
                min: 1, 
                max: 36, 
                message: 'От 1 до 36 мес.', 
                trigger: 'blur'
            },
            { 
            validator: (rule: FormItemRule, value: number, callback: (error?: Error) => void) => {
                if (value != null && filters.value.maxTermValue != null && value > filters.value.maxTermValue) {
                    callback(new Error('Минимальный срок не может быть больше максимального'));
                } else {
                    callback();
                }
            },
            trigger: 'blur'
            }
        ],
        maxTermValue: [
            { 
                type: 'number', 
                min: 1, 
                max: 36, 
                message: 'От 1 до 36 мес.', 
                trigger: 'blur' 
            },
            { 
            validator: (rule: FormItemRule, value: number, callback: (error?: Error) => void) => {
                if (value != null && filters.value.minTermValue != null && value < filters.value.minTermValue) {
                    callback(new Error('Максимальный срок не может быть меньше минимального'));
                } else {
                    callback();
                }
            },
            trigger: 'blur'
            }
        ]
    };

    onMounted(async () => {
        getData();
    });

    const getData = async (filterParams?: IGetLoanApplicationsParams) => {
        loading.value = true

        try {
            tableData.value = await getLoanApplications(filterParams || {});
            tableData.value.reverse();
        }
        finally {
            loading.value = false;
        }
    }

    const editStatus = async (row: LoanApplicationsDirectiryGridDataItem) => {
        try {
            const item = {
                id: row.id,
                status: row.status === LoanStatusEnum.Published ? LoanStatusEnum.Unpublished : LoanStatusEnum.Published
            }

            await editLoanApplicationStatus(item);

            ElMessage.success(`Статус заявки с номером "${row.number}" изменен на "${getLoanStatusLabel(item.status)}"`);

            getData();
        } catch {
            ElMessage.error("Ошибка при изменении статуса заявки");
        }
    }

    const applyFilters = () => {
        filtersForm.value?.validate(async (valid: boolean) => {
            if (!valid) return;

            const filterParams: IGetLoanApplicationsParams = {
                ...(filters.value.status != null && { status: [filters.value.status] }),
                ...(filters.value.minAmount != null && { minAmount: filters.value.minAmount }),
                ...(filters.value.maxAmount != null && { maxAmount: filters.value.maxAmount }),
                ...(filters.value.minTermValue != null && { minTermValue: filters.value.minTermValue }),
                ...(filters.value.maxTermValue != null && { maxTermValue: filters.value.maxTermValue }),
            };

            getData(filterParams);
        });
    }
</script>

<style scoped>
    .filter-item {
        display: flex !important; 
        justify-content: space-between !important; 
        align-items: center !important; 
        gap: 1vh;
    }
</style>