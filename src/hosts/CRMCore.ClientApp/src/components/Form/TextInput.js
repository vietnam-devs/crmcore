import * as React from 'react';
import { Input as ReactStrapInput, FormFeedback } from 'reactstrap';

export const renderTextBoxField = ({
  input,
  label,
  meta: { touched, error, warning }
}) => {
  return (
    <div>
      <ReactStrapInput type={'input'} placeholder={label} {...input} />
      {touched &&
        ((error && (
          <FormFeedback className="text-error b-form-feedback">
            {error}
          </FormFeedback>
        )) ||
          (warning && (
            <FormFeedback className="b-form-feedback">{warning}</FormFeedback>
          )))}
    </div>
  );
};

export const renderNumberField = ({
  input,
  label,
  meta: { touched, error, warning }
}) => {
  return (
    <div>
      <ReactStrapInput type={'number'} placeholder={label} {...input} />
      {touched &&
        ((error && (
          <FormFeedback className="text-error b-form-feedback">
            {error}
          </FormFeedback>
        )) ||
          (warning && (
            <FormFeedback className="b-form-feedback">{warning}</FormFeedback>
          )))}
    </div>
  );
};

export const renderSingleSelectField = ({
  input,
  label,
  meta: { touched, error, warning },
  children
}) => {
  return (
    <div>
      <ReactStrapInput
        type={'select'}
        placeholder={label}
        {...input}
      >
        {children}
      </ReactStrapInput>
      {touched &&
        ((error && (
          <FormFeedback className="text-error b-form-feedback">
            {error}
          </FormFeedback>
        )) ||
          (warning && (
            <FormFeedback className="b-form-feedback">{warning}</FormFeedback>
          )))}
    </div>
  );
};
