import { FC, ReactNode } from 'react';
import ReactDOM from 'react-dom';

import styles from './Modal.module.css';

const portalElement = document.getElementById('overlays') as HTMLDivElement;

export const Backdrop: FC<{ onClose: () => void }> = ({ onClose }) => {
  return <div className={styles.backdrop} onClick={onClose} />;
};

const ModalOverlay: FC<{ children: ReactNode; classes?: string }> = ({ children, classes }) => {
  return <div className={`${styles.modal} ${classes}`}>{children}</div>;
};

const Modal: FC<{ children: ReactNode; onClose: () => void; classes?: string }> = ({
  children,
  onClose,
  classes,
}) => {
  return (
    <>
      {ReactDOM.createPortal(<Backdrop onClose={onClose} />, portalElement)}
      {ReactDOM.createPortal(
        <ModalOverlay classes={classes}>{children}</ModalOverlay>,
        portalElement
      )}
    </>
  );
};

export default Modal;
