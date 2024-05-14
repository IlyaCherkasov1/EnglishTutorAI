import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import timezone from 'dayjs/plugin/timezone';

dayjs.extend(utc);
dayjs.extend(timezone);

export const convertToLocalTime = (date: string | Date) => dayjs(date).utc(true).local();

export const formatDateToISO = (date: string | Date) => convertToLocalTime(date).format("YYYY-MM-DD");