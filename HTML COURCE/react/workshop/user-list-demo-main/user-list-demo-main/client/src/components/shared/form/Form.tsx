import { FC, FormEventHandler, ReactNode } from 'react';

import LoadingSpinner from '../LoadingSpinner';

import styles from './Form.module.css';

const Form: FC<{
  children: ReactNode;
  onSubmitHandler: FormEventHandler<HTMLFormElement>;
  isLoading: boolean;
  classes?: string;
}> = ({ children, onSubmitHandler, isLoading, classes }) => {
  return (
    <>
      <form onSubmit={onSubmitHandler} className={`${styles.form} ${classes}`}>
        {isLoading && <LoadingSpinner className={styles.overlay} />}
        {children}
      </form>
    </>
  );
};

export default Form;
