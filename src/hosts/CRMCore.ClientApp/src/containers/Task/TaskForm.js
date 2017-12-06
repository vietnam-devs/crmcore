import * as React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm } from 'redux-form';

import { Card, CardBody, Form, FormGroup, Label, Button } from 'reactstrap';

// import * as TaskStore from 'redux/modules/task';
import { TextBoxField, SelectField, StandardPanel, PageHeader } from 'components';

class TaskForm extends React.Component {
  render() {
    const { /*error, handleSubmit,*/ pristine, reset, submitting } = this.props;
    return (
      <div className="animated fadeIn">

        <PageHeader title="Task Adding" />

        <StandardPanel noHeader>
          <Form className="b-form">
            <FormGroup>
              <Label for="name">Name</Label>
              <Field
                name="name"
                placeholder="Name"
                type="text"
                component={TextBoxField}
              />
            </FormGroup>
            <FormGroup>
              <Label for="dueType">Due</Label>
              <Field
                name="dueType"
                uri="task-module/api/tasks/due-types"
                component={SelectField}
              />
            </FormGroup>
            <FormGroup>
              <Label for="assignTo">Assign to</Label>
              <Field
                name="assignTo"
                uri="task-module/api/tasks/assign-users"
                component={SelectField}
              />
            </FormGroup>
            <FormGroup>
              <Label for="categoryType">Category</Label>
              <Field
                name="categoryType"
                uri="task-module/api/tasks/category-types"
                component={SelectField}
              />
            </FormGroup>
            <FormGroup>
              <Button
                color="primary"
                type="submit"
                disabled={pristine || submitting}
              >
                <i className="icon-paper-plane b-icon" />Save
              </Button>&nbsp;
              <Button
                color="warning"
                disabled={pristine || submitting}
                onClick={reset}
              >
                <i className="icon-reload b-icon" />Reset
              </Button>&nbsp;
              <Button
                color="link"
                disabled={!pristine}
                onClick={() => {
                  this.props.history.replace('/crm/tasks');
                }}
              >
                Back
              </Button>&nbsp;
            </FormGroup>
          </Form>
        </StandardPanel>
        
      </div>
    );
  }
}

const validate = values => {
  const errors = {};
  return errors;
};

const initData = state => ({
  initialValues: {}
});

export default connect(initData, null)(
  reduxForm({
    form: 'taskForm',
    keepDirtyOnReinitialize: true,
    enableReinitialize: true,
    validate,
    onSubmit: (values, dispatch, props) => {
      //const { match } = props;
    }
  })(TaskForm)
);
