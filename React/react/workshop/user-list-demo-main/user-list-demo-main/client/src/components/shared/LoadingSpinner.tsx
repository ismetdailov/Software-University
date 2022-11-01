import { FC } from 'react';

import styles from './LoadingSpinner.module.css';

const LoadingSpinner: FC<{ className?: string }> = ({ className }) => {
  return <div className={`${styles.spinner} ${className}`} >
    <span className={styles['spinner-text']}>Loading</span>
  </div>;
};

export default LoadingSpinner;
