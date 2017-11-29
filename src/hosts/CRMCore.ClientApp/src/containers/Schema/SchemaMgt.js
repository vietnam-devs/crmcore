import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap';

import { actionCreators as schemaActionCreators } from 'redux/modules/schema';
import SchemaTable from './SchemaTable';

class SchemaMgt extends Component {
  constructor(props) {
    super(props);
    this.addRow = this.addRow.bind(this);
  }

  addRow() {
    this.props.history.push('/metadata/schema-form/$');
  }

  render() {
    var table = (
      <SchemaTable
        totalPages={this.props.totalPages || 1}
        loading={this.props.loading}
        schemas={this.props.schemas}
        loadSchemas={this.props.loadSchemas}
        history={this.props.history}
      />
    );

    return (
      <div className="animated fadeIn">
        <Card>
          <CardBody>
            <Row>
              <Col>
                <div class="list-group">
                  <a
                    href="#"
                    class="list-group-item list-group-item-action flex-column align-items-start active"
                  >
                    <div class="d-flex w-100 justify-content-between">
                      <h5 class="mb-1">List group item heading</h5>
                      <small>3 days ago</small>
                    </div>
                    <p class="mb-1">
                      Donec id elit non mi porta gravida at eget metus. Maecenas
                      sed diam eget risus varius blandit.
                    </p>
                    <small>Donec id elit non mi porta.</small>
                  </a>
                  <a
                    href="#"
                    class="list-group-item list-group-item-action flex-column align-items-start"
                  >
                    <div class="d-flex w-100 justify-content-between">
                      <h5 class="mb-1">List group item heading</h5>
                      <small class="text-muted">3 days ago</small>
                    </div>
                    <p class="mb-1">
                      Donec id elit non mi porta gravida at eget metus. Maecenas
                      sed diam eget risus varius blandit.
                    </p>
                    <small class="text-muted">
                      Donec id elit non mi porta.
                    </small>
                  </a>
                  <a
                    href="#"
                    class="list-group-item list-group-item-action flex-column align-items-start"
                  >
                    <div class="d-flex w-100 justify-content-between">
                      <h5 class="mb-1">List group item heading</h5>
                      <small class="text-muted">3 days ago</small>
                    </div>
                    <p class="mb-1">
                      Donec id elit non mi porta gravida at eget metus. Maecenas
                      sed diam eget risus varius blandit.
                    </p>
                    <small class="text-muted">
                      Donec id elit non mi porta.
                    </small>
                  </a>
                </div>
              </Col>
              <Col>
                <Card className="b-panel">
                  <CardHeader>
                    <h3 className="b-panel-title">
                      <i className="icon-notebook b-icon" />
                      Schema Page
                    </h3>
                    <span className="b-panel-actions">
                      <Button
                        color="success"
                        className="pull-right"
                        onClick={this.addRow}
                      >
                        <i className="icon-plus b-icon" />Add
                      </Button>
                    </span>
                  </CardHeader>
                  <CardBody className="card-body">{table}</CardBody>
                </Card>
              </Col>
            </Row>
          </CardBody>
        </Card>
      </div>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    ...state.schema
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(schemaActionCreators, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(SchemaMgt);
