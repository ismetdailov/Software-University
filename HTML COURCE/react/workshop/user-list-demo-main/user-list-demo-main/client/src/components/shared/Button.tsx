import { FC, ReactNode } from 'react';

import { IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import styles from './Button.module.css';

const Button: FC<{
  children?: ReactNode;
  onClick?: () => void;
  type?: 'button' | 'submit' | 'reset';
  classes?: string;
  disabled?: boolean;
  icon?: IconDefinition | null;
  iconPosition?: 'before' | 'after';
  title?: string;
  action?: 'save' | 'cancel' | 'close';
}> = ({
  children,
  onClick,
  type,
  classes,
  disabled,
  icon,
  title,
  iconPosition = 'before',
  action,
}) => {
  return (
    <button
      type={type || 'button'}
      className={`${styles.btn} ${classes} ${action ? styles[action] : ''}`}
      disabled={disabled}
      onClick={onClick}
      title={title}
    >
      {iconPosition === 'before' && icon && (
        <FontAwesomeIcon icon={icon} className={children ? styles.before : ''} />
      )}
      {children && children}
      {iconPosition === 'after' && icon && (
        <FontAwesomeIcon icon={icon} className={children ? styles.after : ''} />
      )}
    </button>
  );
};

export default Button;
