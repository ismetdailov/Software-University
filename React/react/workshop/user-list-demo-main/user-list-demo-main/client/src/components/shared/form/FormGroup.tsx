import { FC, memo, ReactNode } from 'react';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { IconDefinition } from '@fortawesome/free-solid-svg-icons';

import styles from './FormGroup.module.css';

const FormGroup: FC<{
  label: string;
  name: string;
  icon: IconDefinition;
  children: ReactNode;
  errorMessage: string;
  hasError: boolean;
  isValid: boolean;
  classes?: string;
}> = memo(({ label, name, icon, children, errorMessage, hasError, isValid, classes }) => {
  return (
    <div className={`${styles.group} ${classes}`}>
      <label htmlFor={name}>{label}</label>
      <div
        className={`${styles.control} ${hasError ? styles.invalid : isValid ? styles.valid : ''}`}
      >
        <span>
          <FontAwesomeIcon icon={icon} className={styles.icon} />
        </span>
        {children}
      </div>
      <p className={styles.error}>{hasError && errorMessage}</p>
    </div>
  );
});

export default FormGroup;
