import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { Actions } from 'jumpstate';

import { Field, reduxForm } from 'redux-form';
import { Row, Col, Form, FormGroup, Label, Button } from 'reactstrap';

import {
  TextBoxField,
  SelectField,
  SingleSelectField,
  DateTimeField,
  StandardPanel,
  PageHeader
} from 'components';

class TaskForm extends PureComponent {
  componentDidMount() {
    Actions.tasks.loadCategoryTypes();
  }

  render() {
    const { /*error, handleSubmit,*/ pristine, reset, submitting } = this.props;
    return (
      <div className="animated fadeIn">
        <PageHeader title="Task Adding" />

        <StandardPanel noHeader>
          <Form className="b-form">
            <Row>
              <Col>
                <FormGroup>
                  <Label for="name">Name (*)</Label>
                  <Field
                    name="name"
                    placeholder="Name"
                    type="text"
                    component={TextBoxField}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="start_date">Start Date</Label>
                  <Field
                    name="start_date"
                    placeholder="Input start date time."
                    type="text"
                    formatDateTime={'m/d/Y, h:i K'}
                    component={DateTimeField}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="due_date">Due Date</Label>
                  <Field
                    name="due_date"
                    placeholder="Input due date time."
                    type="text"
                    formatDateTime={'m/d/Y, h:i K'}
                    component={DateTimeField}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="categoryType">Category</Label>
                  <Field
                    name="categoryType"
                    uri="task-module/api/tasks/category-types"
                    component={SingleSelectField}
                  >
                    <option key="option_0">&nbsp;</option>
                    {this.props.categoryKeys.map(catKey => {
                      return (
                        <option
                          key={catKey}
                          value={this.props.categoriesByKey[catKey].key}
                        >
                          {this.props.categoriesByKey[catKey].value}
                        </option>
                      );
                    })}
                  </Field>
                </FormGroup>
              </Col>
              <Col>
                <FormGroup>
                  <Label for="assignTo">Assigned User (*)</Label>
                  <Field
                    name="assignTo"
                    uri="task-module/api/tasks/assign-users"
                    component={SelectField}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="team">Team</Label>
                  <Field
                    name="team"
                    uri="task-module/api/tasks/assign-users"
                    component={SelectField}
                  />
                </FormGroup>
                <FormGroup>
                  <Label for="description">Description</Label>
                  <Field
                    name="description"
                    placeholder="Description"
                    type="textarea"
                    component={TextBoxField}
                  />
                </FormGroup>
              </Col>
            </Row>

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

  if (!values.name) {
    errors.name = 'Required.';
  } else if (values.name.length > 30) {
    errors.name = 'Too long.';
  }

  if (!values.assignTo) {
    errors.assignTo = 'Required.';
  }

  return errors;
};

const initData = state => ({
  categoryKeys: state.task.categoryKeys,
  categoriesByKey: state.task.categoriesByKey,
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
