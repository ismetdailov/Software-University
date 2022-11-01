import { useMemo } from 'react';

export const useTransformDate = (date: string, options?: Intl.DateTimeFormatOptions): string => {
  return useMemo(() => new Date(date).toLocaleString('en', options || {}), [date, options]);
};
